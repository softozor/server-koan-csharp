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

    private string PathToGraphqlFixture(string filenameWithoutExt)
    {
      return Path.Combine(pathToGraphqlFixtures, filenameWithoutExt + ".graphql");
    }

    public string FromFile(string filenameWithoutExt)
    {
      string result = File.ReadAllText(PathToGraphqlFixture(filenameWithoutExt));
      return Regex.Replace(result, @"\n", "");
    }
  }
}
