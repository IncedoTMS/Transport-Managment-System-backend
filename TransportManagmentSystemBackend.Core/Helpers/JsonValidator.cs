using System.Text.Json.Nodes;

namespace TransportManagmentSystemBackend.Core.Helpers
{
    public static class JsonValidator
    {
        public static bool IsJsonString(string json)
        {
            try
            {
                JsonValue.Parse(json);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
