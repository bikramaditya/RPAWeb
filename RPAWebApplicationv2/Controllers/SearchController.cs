using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using LuceneSearch;


namespace RPAWebApplicationv2.Controllers
{
    public class SearchController : Controller
    {
        // GET: /Search/GetResults
        public string GetResults()
        {
            string strIndexPath = System.Configuration.ConfigurationManager.AppSettings["luceneIndexPath"];

            return "Here are your results...";
        }



        // 
        // POST: /Search/UpdateIndex/ 
        public string UpdateIndex(RPAResult sampleDataFileRow)
        {
            
            updateLucene(sampleDataFileRow);

            return "Success";
        }


        private void updateLucene(RPAResult sampleDataFileRow)
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
                if(writer!=null)
                { 
                writer.Optimize();
                writer.Commit();
                writer.Dispose();
                }
            }            
        }

        // 
        // GET: /Search/RebuildIndex/ 
        public string RebuildIndex()
        {
            string strIndexPath = System.Configuration.ConfigurationManager.AppSettings["luceneIndexPath"];

            rebuildLucene(strIndexPath);

            return strIndexPath;
        }

        private void rebuildLucene(String indexPath)
        {
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Directory luceneIndexDirectory;
            IndexWriter writer;

            luceneIndexDirectory = FSDirectory.Open(indexPath);
            
            writer = new IndexWriter(luceneIndexDirectory, analyzer, true, new IndexWriter.MaxFieldLength(500));

            IEnumerable<RPAResult> dataToIndex;
            ISQLReader dataReader = new SQLDataReader();

            dataToIndex = dataReader.ReadAllRows();

            foreach (var sampleDataFileRow in dataToIndex)
            {
                Document doc = new Document();
                doc.Add(new Field("ID", sampleDataFileRow.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("KeyWords", sampleDataFileRow.KeyWords.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("ScriptID", sampleDataFileRow.ScriptID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("ScriptText", sampleDataFileRow.ScriptText, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("UserConfirmationMsg", sampleDataFileRow.UserConfirmationMsg, Field.Store.YES, Field.Index.ANALYZED));
                writer.AddDocument(doc);
            }
            writer.Optimize();
            writer.Commit();
            writer.Dispose();
        }        
    }
}