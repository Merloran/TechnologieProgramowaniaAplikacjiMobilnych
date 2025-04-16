using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using ServerPresentation;

namespace CommandsGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting schema generation...");

            await GenerateSchemaAndCode<GetPlayersCommand>("GetPlayersCommand");
            await GenerateSchemaAndCode<MovePlayerCommand>("MovePlayerCommand");
            await GenerateSchemaAndCode<JoinResponse>("JoinResponse");
            await GenerateSchemaAndCode<PlayerData>("PlayerData");
            await GenerateSchemaAndCode<UpdatePlayersResponse>("UpdatePlayersResponse");
            await GenerateSchemaAndCode<MovePlayerResponse>("MovePlayerResponse");

            Console.WriteLine("Schema generation completed.");
        }

        static async Task GenerateSchemaAndCode<T>(string typeName)
        {
            Console.WriteLine($"Generating schema for {typeName}...");

            JsonSchema schema = JsonSchema.FromType<T>();
            string schemaJson = schema.ToJson();

            string schemaFileName = $"{typeName}_schema.json";
            File.WriteAllText(schemaFileName, schemaJson);
            Console.WriteLine($"Schema saved to {schemaFileName}");

            var settings = new CSharpGeneratorSettings
            {
                Namespace = "Data",
                ClassStyle = CSharpClassStyle.Poco,
                JsonLibrary = CSharpJsonLibrary.NewtonsoftJson,
                GenerateDataAnnotations = false
            };

            JsonSchema loadedSchema = await JsonSchema.FromJsonAsync(schemaJson);
            var generator = new CSharpGenerator(loadedSchema, settings);
            string generatedCode = generator.GenerateFile();

            string codeFileName = $"{typeName}_generated.cs";
            File.WriteAllText(codeFileName, generatedCode);
            Console.WriteLine($"Generated code saved to {codeFileName}");

            Console.WriteLine("Schema JSON:");
            Console.WriteLine(schemaJson);
            Console.WriteLine("\nGenerated C# Code:");
            Console.WriteLine(generatedCode);
            Console.WriteLine("\n----------------------------------------\n");
        }
    }
}