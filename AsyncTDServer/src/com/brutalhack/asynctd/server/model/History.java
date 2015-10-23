package com.brutalhack.asynctd.server.model;

public class History {
    private Round game;
    private Enemies enemies;

    public Round getGame() {
        return game;
    }

    public void setGame(Round game) {
        this.game = game;
    }

    public Enemies getEnemies() {
        return enemies;
    }

    public void setEnemies(Enemies enemies) {
        this.enemies = enemies;
    }
}
