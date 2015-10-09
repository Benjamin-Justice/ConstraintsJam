package com.brutalhack.asynctd.server;

import com.brutalhack.asynctd.server.model.Round;

import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.sql.DataSource;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

public class DatabaseAdapterSingleton {
    private static final String GET_GAME_SQL = "SELECT fbid,parent,depth,action " +
            "FROM asynctd.game " +
            "WHERE fbid = ? " +
            "OR parent = (Select id from asynctd.game WHERE fbid = ?) " +
            "AND fbid = ?" +
            "OR parent = (Select id from asynctd.game WHERE fbid = ?) " +
            "AND fbid = ?" +
            "OR parent = (Select id from asynctd.game WHERE fbid = ?) " +
            "AND fbid = ?" +
            "OR parent = (Select id from asynctd.game WHERE fbid = ?) " +
            "AND fbid = ?" +
            "OR parent = (Select id from asynctd.game WHERE fbid = ?) " +
            "AND fbid = ?" +
            "OR parent = (Select id from asynctd.game WHERE fbid = ?) " +
            "AND fbid = ?" +
            "OR parent = (Select id from asynctd.game WHERE fbid = ?) " +
            "AND fbid = ?" +
            "ORDER BY depth DESC";
    private static final String GET_PARENT_SQL = "SELECT id, depth  " +
            "            FROM asynctd.game  " +
            "            WHERE fbid = ?  " +
            "            OR parent = (Select id from asynctd.game WHERE fbid = ?)  " +
            "            AND fbid = ? " +
            "            OR parent = (Select id from asynctd.game WHERE fbid = ?)  " +
            "            AND fbid = ? " +
            "            OR parent = (Select id from asynctd.game WHERE fbid = ?)  " +
            "            AND fbid = ? " +
            "            OR parent = (Select id from asynctd.game WHERE fbid = ?)  " +
            "            AND fbid = ? " +
            "            OR parent = (Select id from asynctd.game WHERE fbid = ?)  " +
            "            AND fbid = ? " +
            "            OR parent = (Select id from asynctd.game WHERE fbid = ?)  " +
            "            AND fbid = ? " +
            "            OR parent = (Select id from asynctd.game WHERE fbid = ?)  " +
            "            AND fbid = ? " +
            "            ORDER BY depth DESC" +
            "             LIMIT 1";
    private static final String INSERT_ROUND_SQL =
            "INSERT INTO asynctd.game (fbid,parent,depth,action) VALUES(?,?,?,?)";
    private static DatabaseAdapterSingleton instance;
    private Connection connection;
    private PreparedStatement getGameStatement;
    private final PreparedStatement getParentStatement;
    private final PreparedStatement insertRoundStatement;

    public static synchronized DatabaseAdapterSingleton getInstance() {
        if (instance == null) {
            instance = new DatabaseAdapterSingleton();
        }
        return instance;
    }

    private DatabaseAdapterSingleton() {
        initConnection();
        try {
            getGameStatement = connection.prepareStatement(
                    GET_GAME_SQL);
            System.out.println(getGameStatement);
            getParentStatement = connection.prepareStatement(GET_PARENT_SQL);
            System.out.println(getParentStatement);
            insertRoundStatement = connection.prepareStatement(INSERT_ROUND_SQL);
            System.out.println(insertRoundStatement);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    public synchronized Round getRound(List<String> ids) {
        try {
            addIdsParameters(ids, getGameStatement);
            System.out.println(getGameStatement.toString());
            try (ResultSet resultSet = getGameStatement.executeQuery()) {
                Round round = null;
                int size = 0;
                while (resultSet.next()) {
                    Round newRound = new Round();
                    newRound.setFacebookId(resultSet.getString("fbid"));
                    System.out.println(newRound.getFacebookId());
                    newRound.setPlayerActions(resultSet.getString("action"));
                    newRound.setChildRound(round);
                    round = newRound;
                    size++;
                }
                if (size != ids.size()) {
                    return null;
                }
                return round;
            }
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    private void addIdsParameters(List<String> ids, PreparedStatement getGameStatement) throws SQLException {
        fillAllParameter(getGameStatement);
        System.out.println(ids);
        for (int i = 1; i < ids.size(); i++) {
            getGameStatement.setString((2 * i) - 1, ids.get(i - 1));
            getGameStatement.setString((2 * i), ids.get(i - 1));
        }
        getGameStatement.setString((2 * ids.size()) - 1, ids.get(ids.size() - 1));
    }

    private void fillAllParameter(PreparedStatement preparedStatement) throws SQLException {
        for (int i = 1; i <= 15; i++) {
            preparedStatement.setString(i, "");
        }
    }

    private DataSource getDataSource() {
        InitialContext cxt;
        DataSource ds;
        try {
            cxt = new InitialContext();
            ds = (DataSource) cxt.lookup("java:/comp/env/jdbc/postgres");
        } catch (NamingException e) {
            throw new RuntimeException(e);
        }
        return ds;
    }

    private void initConnection() {
        DataSource ds = getDataSource();
        try {
            connection = ds.getConnection();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    public void addRound(List<String> ids, Round round) {
        if (round == null) {
            System.out.println("round was null");
            return;
        }
        try {
            addIdsParameters(ids, getParentStatement);
            System.out.println(getParentStatement.toString());
            try (ResultSet resultSet = getParentStatement.executeQuery()) {
                resultSet.next();
                int parentId = resultSet.getInt(1);
                int parentDepth = resultSet.getInt(2);
                System.out.println(insertRoundStatement);
                System.out.println(round);
                insertRoundStatement.setString(1, round.getFacebookId());
                insertRoundStatement.setInt(2, parentId);
                insertRoundStatement.setInt(3, parentDepth + 1);
                insertRoundStatement.setString(4, round.getPlayerActions());
                insertRoundStatement.executeUpdate();
            }
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }
}
