using System.IO;
using System.Text.RegularExpressions;

namespace Utils
{
  public class GraphqlLoader
  {
    private readonly string pathToGraphqlFixtures;

    public GraphqlLoader(string pathToGraphqlFixtures)
    {
      this.pathToGraphqlFixtures = pathToGraphqlFixtures;
    }

    public string FromFile(string filenameWithoutExt)
    {
      string result = File.ReadAllText(Path.Combine(pathToGraphqlFixtures, filenameWithoutExt + ".graphql"));
      return Regex.Replace(result, @"\n", "");
    }
  }
}
