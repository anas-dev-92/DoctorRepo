using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services.FAQ
{
    public class FAQRepository : IFAQRepository
    {
        private readonly DoctorsDbContext _context;

        public FAQRepository(DoctorsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateFAQ(FAQs faqs)
        {
            try
            {
                _context.fAQss.Add(faqs);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public void DeleteFAQ(FAQs faqs)
        {
            _context.Remove(faqs);
        }

        public IEnumerable<FAQs> GetFAQ()
        {
            return _context.fAQss.ToList();
        }

        public FAQs GetFAQs(int faqsId)
        {
            return _context.fAQss.Where(u => u.FaqId == faqsId).FirstOrDefault();
        }

        public void Savechnge()
        {
            _context.SaveChanges();
        }

        public void UpdateFAQ(int faqId, FAQs faqs)
        {

        }

        public bool FAQExists(int faqId)
        {
            return _context.fAQss.Any(u => u.FaqId == faqId);
        }
    }
}
