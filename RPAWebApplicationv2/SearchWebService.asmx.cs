using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneSearch;


namespace RPAWebApplicationv2
{
    /// <summary>
    /// Summary description for SearchWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SearchWebService : System.Web.Services.WebService
    {        
        [WebMethod]
        public RPATicket GetSearchResults(RPATicket ticket)
        {
            string strIndexPath = System.Configuration.ConfigurationManager.AppSettings["luceneIndexPath"];

            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            string searchTerm = "";

            foreach (String category in ticket.Categories)
            {
                searchTerm += " " + category;
            }
            searchTerm = searchTerm.Trim();

            Directory luceneIndexDirectory = FSDirectory.Open(strIndexPath);
            IndexSearcher searcher = new IndexSearcher(luceneIndexDirectory);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "KeyWords", analyzer);

            Query query = parser.Parse(searchTerm);

            TopDocs hitsFound = searcher.Search(query, 200);
            List<RPAResult> results = new List<RPAResult>();
            RPAResult _result = null;

            for (int i = 0; i < hitsFound.TotalHits; i++)
            {
                _result = new RPAResult();
                Document doc = searcher.Doc(hitsFound.ScoreDocs[i].Doc);
                _result.ID = int.Parse(doc.Get("ID"));
                _result.ScriptID = int.Parse(doc.Get("ScriptID"));
                _result.ScriptText = doc.Get("ScriptText");
                _result.KeyWords = doc.Get("KeyWords");
                _result.UserConfirmationMsg = doc.Get("UserConfirmationMsg");
                float score = hitsFound.ScoreDocs[i].Score;
                _result.Score = score;
                results.Add(_result);
            }
            ticket.Matches = results;
            return ticket;
        }

        [WebMethod]
        public void UpdateLucene(RPAResult sampleDataFileRow)
        {
            string strIndexPath = System.Configuration.ConfigurationManager.AppSettings["luceneIndexPath"];

            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Directory luceneIndexDirectory;
            IndexWriter writer = null;
            try
            {
                luceneIndexDirectory = FSDirectory.Open(strIndexPath);

                writer = new IndexWriter(luceneIndexDirectory, analyzer, true, new IndexWriter.MaxFieldLength(500));

                Document doc = new Document();
                doc.Add(new Field("ScriptID", sampleDataFileRow.ScriptID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("ScriptText", sampleDataFileRow.ScriptText, Field.Store.YES, Field.Index.ANALYZED));
                writer.AddDocument(doc);
            }
            catch (Exception)
            { }
            finally
            {
                if (writer != null)
                {
                    writer.Optimize();
                    writer.Commit();
                    writer.Dispose();
                }
            }
        }
    }
}
