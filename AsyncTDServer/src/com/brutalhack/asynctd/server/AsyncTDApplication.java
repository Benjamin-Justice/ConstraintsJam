package com.brutalhack.asynctd.server;


import javax.ws.rs.core.Application;
import java.util.LinkedHashSet;
import java.util.Set;

public class AsyncTDApplication extends Application {
    @Override
    public Set<Class<?>> getClasses() {
        Set<Class<?>> resources = new LinkedHashSet<>();
        resources.add(org.glassfish.jersey.jackson.JacksonFeature.class);
        resources.add(JacksonMapper.class);
        resources.add(GameResource.class);
        return resources;
    }
}
