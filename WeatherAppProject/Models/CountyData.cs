namespace WeatherAppProject.Models;

public static class CountyData
{
    public static readonly Dictionary<string, (double Latitude, double Longitude)> Counties = new()
    {
        ["Antrim"] = (54.7195, -6.2072),
        ["Armagh"] = (54.3503, -6.6528),
        ["Carlow"] = (52.8408, -6.9261),
        ["Cavan"] = (53.9908, -7.3600),
        ["Clare"] = (52.8436, -8.9864),
        ["Cork"] = (51.8985, -8.4756),
        ["Derry"] = (54.9972, -7.3092),
        ["Donegal"] = (54.6538, -8.1105),
        ["Down"] = (54.3190, -5.7034),
        ["Dublin"] = (53.3498, -6.2603),
        ["Fermanagh"] = (54.3448, -7.6386),
        ["Galway"] = (53.2707, -9.0568),
        ["Kerry"] = (52.2713, -9.6863),
        ["Kildare"] = (53.1589, -6.9096),
        ["Kilkenny"] = (52.6541, -7.2448),
        ["Laois"] = (53.0320, -7.3000),
        ["Leitrim"] = (54.0499, -8.0570),
        ["Limerick"] = (52.6638, -8.6267),
        ["Longford"] = (53.7270, -7.7930),
        ["Louth"] = (53.9980, -6.4050),
        ["Mayo"] = (53.8500, -9.3000),
        ["Meath"] = (53.6528, -6.6814),
        ["Monaghan"] = (54.2492, -6.9683),
        ["Offaly"] = (53.2734, -7.4889),
        ["Roscommon"] = (53.6279, -8.1890),
        ["Sligo"] = (54.2766, -8.4761),
        ["Tipperary"] = (52.4738, -8.1619),
        ["Tyrone"] = (54.6000, -7.3000),
        ["Waterford"] = (52.2593, -7.1101),
        ["Westmeath"] = (53.5345, -7.4653),
        ["Wexford"] = (52.3369, -6.4633),
        ["Wicklow"] = (52.9808, -6.0446)
    };
}