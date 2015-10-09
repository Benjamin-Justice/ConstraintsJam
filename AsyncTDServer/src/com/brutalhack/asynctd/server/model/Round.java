package com.brutalhack.asynctd.server.model;

public class Round {
    private String playerActions;
    private Round childRound;
    private String facebookId;

    public String getFacebookId() {
        return facebookId;
    }

    public void setFacebookId(String facebookId) {
        this.facebookId = facebookId;
    }


    public String getPlayerActions() {
        return playerActions;
    }

    public void setPlayerActions(String playerActions) {
        this.playerActions = playerActions;
    }

    public Round getChildRounds() {
        return childRound;
    }

    public void setChildRound(Round childRound) {
        this.childRound = childRound;
    }
}
