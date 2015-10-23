package com.brutalhack.asynctd.server.model;

import java.util.ArrayList;
import java.util.List;

public class Enemies {
    private List<Wave> waves = new ArrayList<>();

    public List<Wave> getWaves() {
        return waves;
    }

    public void setWaves(List<Wave> waves) {
        this.waves = waves;
    }
    public void addWave(Wave wave){
        waves.add(wave);
    }
}
