using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace StravaData
{
    public class StravaActivity
    {
        [JsonProperty("resource_state")] public int ResourceState { get; set; }

        [JsonProperty("athlete")] public MetaAthlete Athlete { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("distance")] public double Distance { get; set; }

        [JsonProperty("moving_time")] public int MovingTime { get; set; }

        [JsonProperty("elapsed_time")] public int ElapsedTime { get; set; }

        [JsonProperty("total_elevation_gain")] public double TotalElevationGain { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("sport_type")] public string SportType { get; set; }

        [JsonProperty("workout_type")] public int? WorkoutType { get; set; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("start_date")] public DateTime StartDate { get; set; }

        [JsonProperty("start_date_local")] public DateTime StartDateLocal { get; set; }

        [JsonProperty("timezone")] public string Timezone { get; set; }

        [JsonProperty("utc_offset")] public double UtcOffset { get; set; }

        [JsonProperty("map")] public Map Map { get; set; }

        [JsonProperty("trainer")] public bool Trainer { get; set; }

        [JsonProperty("commute")] public bool Commute { get; set; }

        [JsonProperty("manual")] public bool Manual { get; set; }

        [JsonProperty("private")] public bool Private { get; set; }

        [JsonProperty("visibility")] public string Visibility { get; set; }

        [JsonProperty("flagged")] public bool Flagged { get; set; }

        [JsonProperty("gear_id")] public string GearId { get; set; }

        [JsonProperty("start_latlng")] public List<double> StartLatlng { get; set; }

        [JsonProperty("end_latlng")] public List<double> EndLatlng { get; set; }

        [JsonProperty("average_speed")] public double AverageSpeed { get; set; }

        [JsonProperty("max_speed")] public double MaxSpeed { get; set; }

        [JsonProperty("average_watts")] public double AverageWatts { get; set; }

        [JsonProperty("device_watts")] public bool DeviceWatts { get; set; }

        [JsonProperty("kilojoules")] public double Kilojoules { get; set; }

        [JsonProperty("has_heartrate")] public bool HasHeartrate { get; set; }

        [JsonProperty("average_heartrate")] public double AverageHeartrate { get; set; }

        [JsonProperty("max_heartrate")] public double MaxHeartrate { get; set; }

        [JsonProperty("elev_high")] public double ElevHigh { get; set; }

        [JsonProperty("elev_low")] public double ElevLow { get; set; }

        [JsonProperty("pr_count")] public int PrCount { get; set; }

        [JsonProperty("total_photo_count")] public int TotalPhotoCount { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("calories")] public double Calories { get; set; }

        [JsonProperty("segment_efforts")] public List<SegmentEffort> SegmentEfforts { get; set; }

        [JsonProperty("splits_metric")] public List<Split> SplitsMetric { get; set; }

        [JsonProperty("splits_standard")] public List<Split> SplitsStandard { get; set; }

        [JsonProperty("laps")] public List<Lap> Laps { get; set; }

        [JsonProperty("gear")] public Gear Gear { get; set; }

        [JsonProperty("photos")] public PhotosContainer Photos { get; set; }

        [JsonProperty("stats_visibility")] public List<StatsVisibility> StatsVisibility { get; set; }

        [JsonProperty("available_zones")] public List<string> AvailableZones { get; set; }
    }

    public class MetaAthlete
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("resource_state")] public int ResourceState { get; set; }
    }

    public class Map
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("polyline")] public string Polyline { get; set; }

        [JsonProperty("resource_state")] public int ResourceState { get; set; }

        [JsonProperty("summary_polyline")] public string SummaryPolyline { get; set; }
    }

    public class SegmentEffort
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("resource_state")] public int ResourceState { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("activity")] public MetaActivity Activity { get; set; }

        [JsonProperty("athlete")] public MetaAthlete Athlete { get; set; }

        [JsonProperty("elapsed_time")] public int ElapsedTime { get; set; }

        [JsonProperty("moving_time")] public int MovingTime { get; set; }

        [JsonProperty("start_date")] public DateTime StartDate { get; set; }

        [JsonProperty("start_date_local")] public DateTime StartDateLocal { get; set; }

        [JsonProperty("distance")] public double Distance { get; set; }

        [JsonProperty("start_index")] public int StartIndex { get; set; }

        [JsonProperty("end_index")] public int EndIndex { get; set; }

        [JsonProperty("average_heartrate")] public double AverageHeartrate { get; set; }

        [JsonProperty("max_heartrate")] public double MaxHeartrate { get; set; }

        [JsonProperty("segment")] public Segment Segment { get; set; }

        [JsonProperty("pr_rank")] public int? PrRank { get; set; }

        [JsonProperty("achievements")] public List<Achievement> Achievements { get; set; }

        [JsonProperty("hidden")] public bool? Hidden { get; set; }
    }

    public class MetaActivity
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("visibility")] public string Visibility { get; set; }

        [JsonProperty("resource_state")] public int ResourceState { get; set; }
    }

    public class Segment
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("resource_state")] public int ResourceState { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("activity_type")] public string ActivityType { get; set; }

        [JsonProperty("distance")] public double Distance { get; set; }

        [JsonProperty("average_grade")] public double AverageGrade { get; set; }

        [JsonProperty("maximum_grade")] public double MaximumGrade { get; set; }

        [JsonProperty("elevation_high")] public double ElevationHigh { get; set; }

        [JsonProperty("elevation_low")] public double ElevationLow { get; set; }

        [JsonProperty("start_latlng")] public List<double> StartLatlng { get; set; }

        [JsonProperty("end_latlng")] public List<double> EndLatlng { get; set; }

        [JsonProperty("climb_category")] public int ClimbCategory { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("private")] public bool Private { get; set; }

        [JsonProperty("hazardous")] public bool Hazardous { get; set; }

        [JsonProperty("starred")] public bool Starred { get; set; }
    }

    public class Achievement
    {
        [JsonProperty("type_id")] public int TypeId { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("rank")] public int Rank { get; set; }
    }

    public class Split
    {
        [JsonProperty("distance")] public double Distance { get; set; }

        [JsonProperty("elapsed_time")] public int ElapsedTime { get; set; }

        [JsonProperty("elevation_difference")] public double ElevationDifference { get; set; }

        [JsonProperty("moving_time")] public int MovingTime { get; set; }

        [JsonProperty("split")] public int SplitIndex { get; set; }

        [JsonProperty("average_speed")] public double AverageSpeed { get; set; }

        [JsonProperty("average_grade_adjusted_speed")]
        public double AverageGradeAdjustedSpeed { get; set; }

        [JsonProperty("average_heartrate")] public double AverageHeartrate { get; set; }

        [JsonProperty("pace_zone")] public int PaceZone { get; set; }
    }

    public class Lap
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("resource_state")] public int ResourceState { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("elapsed_time")] public int ElapsedTime { get; set; }

        [JsonProperty("moving_time")] public int MovingTime { get; set; }

        [JsonProperty("start_date")] public DateTime StartDate { get; set; }

        [JsonProperty("start_date_local")] public DateTime StartDateLocal { get; set; }

        [JsonProperty("distance")] public double Distance { get; set; }

        [JsonProperty("average_speed")] public double AverageSpeed { get; set; }

        [JsonProperty("max_speed")] public double MaxSpeed { get; set; }

        [JsonProperty("lap_index")] public int LapIndex { get; set; }

        [JsonProperty("split")] public int Split { get; set; }

        [JsonProperty("start_index")] public int StartIndex { get; set; }

        [JsonProperty("end_index")] public int EndIndex { get; set; }

        [JsonProperty("total_elevation_gain")] public double TotalElevationGain { get; set; }

        [JsonProperty("average_watts")] public double AverageWatts { get; set; }

        [JsonProperty("average_heartrate")] public double AverageHeartrate { get; set; }

        [JsonProperty("max_heartrate")] public double MaxHeartrate { get; set; }
    }

    public class Gear
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("primary")] public bool Primary { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("nickname")] public string Nickname { get; set; }

        [JsonProperty("resource_state")] public int ResourceState { get; set; }

        [JsonProperty("retired")] public bool Retired { get; set; }

        [JsonProperty("distance")] public double Distance { get; set; }

        [JsonProperty("converted_distance")] public double ConvertedDistance { get; set; }
    }

    public class PhotosContainer
    {
        [JsonProperty("primary")] public PhotoPrimary Primary { get; set; }

        [JsonProperty("use_primary_photo")] public bool UsePrimaryPhoto { get; set; }

        [JsonProperty("count")] public int Count { get; set; }
    }

    public class PhotoPrimary
    {
        [JsonProperty("unique_id")] public string UniqueId { get; set; }

        [JsonProperty("urls")] public Dictionary<string, string> Urls { get; set; }

        [JsonProperty("source")] public int Source { get; set; }

        [JsonProperty("media_type")] public int MediaType { get; set; }
    }

    public class StatsVisibility
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("visibility")] public string Visibility { get; set; }
    }

    public class ActivityStreams
    {
        [JsonProperty("latlng")] public LatLngStream LatLng { get; set; }

        [JsonProperty("altitude")] public FloatStream Altitude { get; set; }

        [JsonProperty("time")] public IntStream Time { get; set; }

        [JsonProperty("distance")] public FloatStream Distance { get; set; }

        // Дополнительные потоки, которые часто встречаются (на случай, если они появятся)
        [JsonProperty("heartrate")] public IntStream HeartRate { get; set; }

        [JsonProperty("watts")] public IntStream Watts { get; set; }

        [JsonProperty("velocity_smooth")] public FloatStream Velocity { get; set; }

        [JsonProperty("cadence")] public IntStream Cadence { get; set; }

        [JsonProperty("temp")] public IntStream Temperature { get; set; }

        [JsonProperty("moving")] public BoolStream Moving { get; set; }

        [JsonProperty("grade_smooth")] public FloatStream Grade { get; set; }
    }

    // Базовый класс для потоков данных
    public class StreamData<T>
    {
        [JsonProperty("data")] public List<T> Data { get; set; }

        [JsonProperty("series_type")] public string SeriesType { get; set; }

        [JsonProperty("original_size")] public int OriginalSize { get; set; }

        [JsonProperty("resolution")] public string Resolution { get; set; }
    }

    // Специализированные классы для удобства и обработки типов

    // 1. Поток координат. В JSON это массив массивов [[lat, lng], [lat, lng]].
    // Для Unity удобно сразу конвертировать это в Vector2, но Json.NET
    // не умеет сам превращать double[] в Vector2. Поэтому используем List<double[]> для сырых данных
    // или пишем кастомный конвертер. Самый простой способ без лишнего кода - принять как List<double[]>
    public class LatLngStream : StreamData<double[]>
    {
        // Вспомогательный метод для получения данных в формате Unity Vector2 (x = lat, y = lng)
        public List<Vector2> GetAsVector2List()
        {
            if (Data == null) return new List<Vector2>();
            var list = new List<Vector2>(Data.Count);
            foreach (var point in Data)
            {
                if (point.Length >= 2)
                {
                    // Обратите внимание: Strava отдает [Lat, Lng].
                    // В Unity X обычно горизонталь, Y вертикаль.
                    // Для карт часто X=Lng, Y=Lat.
                    // Здесь сохраняем "как есть": x=Lat, y=Lng.
                    list.Add(new Vector2((float)point[0], (float)point[1]));
                }
            }

            return list;
        }
    }

    // 2. Поток дробных чисел (дистанция, высота, скорость)
    public class FloatStream : StreamData<float>
    {
    }

    // 3. Поток целых чисел (время, пульс, ватты, каденс)
    public class IntStream : StreamData<int>
    {
    }

    // 4. Поток булевых значений (движется/стоит)
    public class BoolStream : StreamData<bool>
    {
    }

    public class PolylineMap
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("summary_polyline")]
        public string SummaryPolyline { get; set; }

        [JsonProperty("resource_state")]
        public int ResourceState { get; set; }
    }
}