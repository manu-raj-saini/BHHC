using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHHCSampleApi.Controllers
{
    /// <summary>
    /// Interface for providing functnality for Reason Access
    /// </summary>
    public interface IReason
    {
        Models.ReasonContext Context { get; }
        DbSet<Models.ReasonItem> GetReasons();
        int GetReasonsCount();
        Models.ReasonItem GetReason(long id);
        void CreateReason(Models.ReasonItem reason);
        bool CheckReasonExist(int id);
    }
    public class Reason : IReason
    {
        Models.ReasonContext _context;
        public Models.ReasonContext Context { get; }
        public Reason(Models.ReasonContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get All Reason
        /// </summary>
        /// <returns></returns>
        public DbSet<Models.ReasonItem> GetReasons()
        {
            return _context.ReasonItems;
        }
        /// <summary>
        /// Get All Reason
        /// </summary>
        /// <returns></returns>
        public int GetReasonsCount()
        {
            return _context.ReasonItems.Count();
        }
        /// <summary>
        /// Get Reason on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.ReasonItem GetReason(long id)
        {
            var reason = _context.ReasonItems.Find(id);
            return reason;
        }
        /// <summary>
        /// Create Initial Reason
        /// </summary>
        /// <param name="reason"></param>
        public void CreateReasons()
        {
            if (_context.ReasonItems.Count() == 0)
            {
                // Create a new ReasonItem if collection is empty,
                // which means you can't delete all ReasonItems.
                CreateReason(new Models.ReasonItem { Name = Models.ReasonContext.REASON1 });
                CreateReason(new Models.ReasonItem { Name = Models.ReasonContext.REASON2 });
                CreateReason(new Models.ReasonItem { Name = Models.ReasonContext.REASON3 });
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Create Reason
        /// </summary>
        /// <param name="reason"></param>
        public void CreateReason(Models.ReasonItem reason)
        {
            _context.ReasonItems.Add(reason);
        }
        public bool CheckReasonExist(int id)
        {
            return true;
        }
    }
}
