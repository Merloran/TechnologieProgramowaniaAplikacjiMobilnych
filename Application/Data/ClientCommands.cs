using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Data
{
    public static class Headers
    {
        public static string GetPlayersCommand     = "GetPlayers";
        public static string MovePlayerCommand     = "MovePlayer";
        public static string JoinResponse          = "JoinResponse";
        public static string UpdatePlayersResponse = "UpdatePlayers";
        public static string MovePlayerResponse    = "MovePlayerResponse";
        public static string GetHeader(string json)
        {
            JObject obj = JObject.Parse(json);
            if (obj.TryGetValue("Header", out JToken? value))
            {
                return value.ToString();
            }
            return null;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public abstract partial class ClientCommand
    {
        [Newtonsoft.Json.JsonProperty("Header", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Header { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class GetPlayersCommand : ClientCommand
    {
    }

    [Serializable]
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class MovePlayerCommand : ClientCommand
    {
        [Newtonsoft.Json.JsonProperty("transactionId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid TransactionId { get; set; }

        [Newtonsoft.Json.JsonProperty("playerId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid PlayerId { get; set; }

        [Newtonsoft.Json.JsonProperty("direction", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Direction Direction { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public abstract partial class ServerResponse
    {
        [Newtonsoft.Json.JsonProperty("Header", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Header { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class JoinResponse : ServerResponse
    {
        [Newtonsoft.Json.JsonProperty("guidForPlayer", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid GuidForPlayer { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class PlayerData
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("x", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float X { get; set; }

        [Newtonsoft.Json.JsonProperty("y", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float Y { get; set; }

        [Newtonsoft.Json.JsonProperty("speed", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float Speed { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class UpdatePlayersResponse : ServerResponse
    {
        [Newtonsoft.Json.JsonProperty("players", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<PlayerData> Players { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class MovePlayerResponse : ServerResponse
    {
        [Newtonsoft.Json.JsonProperty("transactionId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid TransactionId { get; set; }

        [Newtonsoft.Json.JsonProperty("isSuccess", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool IsSuccess { get; set; }
    }
}
