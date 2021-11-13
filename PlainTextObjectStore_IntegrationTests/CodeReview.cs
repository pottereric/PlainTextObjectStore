using PlainTextObjectStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainTextObjectStore_IntegrationTests
{
    internal class CodeReview : IRecord
    {
        public string Key { 
            get => $"{ProjectName}--{ID}--{CreatorName}";
            set => _ = value; 
        }

        public string CreatorName { get; set; } = String.Empty;
        public string ProjectName { get; set; } = String.Empty;
        public int ID { get; set; }
    }
}
