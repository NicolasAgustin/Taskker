using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Taskker.Models.DAL;

namespace Taskker.Models.Services
{
    public interface INotesService
    {
        Task<List<Note>> GetNotes(int userid);
        Task<bool> Auth(string user, string password);
        Task<bool> Create(string text, int userid);
    }
}