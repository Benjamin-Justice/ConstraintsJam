package com.brutalhack.asynctd.server;

import com.brutalhack.asynctd.server.model.Round;

import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import java.util.List;

@Path("game")
public class GameResource {
    private DatabaseAdapterSingleton databaseAdapter;

    public GameResource() {
        databaseAdapter = DatabaseAdapterSingleton.getInstance();
    }

    @GET
    @QueryParam("id")
    @Produces(MediaType.APPLICATION_JSON)
    public Round getGame(@QueryParam("id") List<String> ids) {
        return databaseAdapter.getRound(ids);
    }

    @PUT
    @QueryParam("id")
    @Consumes(MediaType.APPLICATION_JSON)
    public void addRoundPut(@QueryParam("id") List<String> ids, Round round) {
        databaseAdapter.addRound(ids,round);
    }

}
