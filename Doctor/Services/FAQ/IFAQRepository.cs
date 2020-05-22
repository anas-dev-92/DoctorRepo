using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services.FAQ
{
    public interface IFAQRepository
    {
        IEnumerable<FAQs> GetFAQ();
        FAQs GetFAQs(int faqId);
        bool FAQExists(int faqId);
        void CreateFAQ(FAQs faq);
        void UpdateFAQ(int faqId, FAQs faq);
        void DeleteFAQ(FAQs faq);
        void Savechnge();
    }
}
