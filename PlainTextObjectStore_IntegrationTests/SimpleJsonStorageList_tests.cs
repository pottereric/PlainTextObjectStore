namespace PlainTextObjectStore_IntegrationTests;

[TestClass]
public class SimpleJsonStorageList_tests
{
    [TestMethod]
    public void T1_CanCreateFiles()
    {
        var testReviewA = new CodeReview() { CreatorName = "Don Syme", ID = 7, ProjectName = "F#" };
        var testReviewB = new CodeReview() { CreatorName = "Anders", ID = 7, ProjectName = "C#" };
        var testReviewC = new CodeReview() { CreatorName = "Anders", ID = 7, ProjectName = "TypeScript" };

        var testList = new JsonBackedDictionary<CodeReview>("testObjects");
        testList.Add(testReviewA);
        testList.Add(testReviewB);
        testList.Add(testReviewC);

        var fileCount = Directory.GetFiles("testObjects").Length;
        Assert.AreEqual(3, fileCount);
    }

    [TestMethod]
    public void T2_CanLoadFiles()
    {
        var testList = new JsonBackedDictionary<CodeReview>("testObjects");

        var fileCount = Directory.GetFiles("testObjects").Length;
        Assert.AreEqual(3, fileCount);
    }

    [TestMethod]
    public void T3_CanClearFiles()
    {
        var testList = new JsonBackedDictionary<CodeReview>("testObjects");

        testList.Clear();

        var fileCount = Directory.GetFiles("testObjects").Length;
        Assert.AreEqual(0, fileCount);

    }
}