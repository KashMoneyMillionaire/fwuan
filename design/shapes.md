# Shapes
--
In fwuan, shapes are one of the ways of representing a data structure, similar to an interface:

```
public shape GeoPoint
{
    public double Lat;
    public double Lon;
}
```

When used as a parameter, any data structure passed in that resembles it will be valid:

```
public function IsValid(geo: GeoPoint): bool
{
    if (geo.Lat != 666)
        return true;
        
    return false;
}

public static function Test()
{
    GeoPoint geoShape =
    {
        Lat = 123,
        Lon = 345
    };
    
    Dictionary<string, string> geoDic = 
    {
        ["Lat"] = 444,
        ["Lon"] = 532
    }
    
    (string, double)[] geoTuples = 
    {
        ("Lat", 999),
        ("Lon", 944)
    }
    
    anon geoAnonymous =
    {
        Lat = 1,
        Long = 2
    }
    
    anon geoReshape =
    {
        Latitude = 22,
        Longitude = 53
    }
    
    // All of the following work
    IsValid(geoShapre);
    IsValid(geoDic);
    IsValid(geoTuples);
    IsValid(geoAnonymous);
    IsValid(geoReshapre with {Latitude as Lat, Longitude as Long});
}

```

As you can see, anything that has the right shape will fit into the function.
