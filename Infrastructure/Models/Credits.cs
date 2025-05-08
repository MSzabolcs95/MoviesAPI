using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class Credits
{
    [JsonProperty("cast")]
    public List<CastMember> Cast { get; set; } = new List<CastMember>();

    [JsonProperty("crew")]
    public List<CrewMember> Crew { get; set; } = new List<CrewMember>();
}

public class CastMember
{
    [JsonProperty("cast_id")]
    public int CastId { get; set; }

    [JsonProperty("character")]
    public string Character { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("profile_path")]
    public string ProfilePath { get; set; } = string.Empty;

    [JsonProperty("order")]
    public int Order { get; set; }
}

public class CrewMember
{
    [JsonProperty("department")]
    public string Department { get; set; } = string.Empty;

    [JsonProperty("job")]
    public string Job { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("profile_path")]
    public string ProfilePath { get; set; } = string.Empty;
}
