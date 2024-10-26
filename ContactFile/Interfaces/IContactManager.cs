using ContactFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFile.Interfaces
{
    public interface IContactManager
    {
        void AddContact(Contact contact);
        void EditContact(string phoneNumber, Contact updatedContact);
        void DeleteContact(string phoneNumber);
    }
}