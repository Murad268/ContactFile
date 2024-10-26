using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ContactFile.Interfaces;
using ContactFile.Models;

namespace ContactFile.Controllers
{
    public class ContactController : IContactManager
    {
        private const string FilePath = "contact.txt";

        public void AddContact(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name) || string.IsNullOrWhiteSpace(contact.Surname) || string.IsNullOrWhiteSpace(contact.PhoneNumber))
            {
                Console.WriteLine("Invalid contact details. All fields are required.");
                return;
            }

            var contacts = GetAllContacts();
            if (contacts.Any(c => c.PhoneNumber == contact.PhoneNumber))
            {
                Console.WriteLine("A contact with this phone number already exists.");
                return;
            }

            contacts.Add(contact);
            SaveContactsToFile(contacts);
            Console.WriteLine("Contact added successfully.");
        }

        public void EditContact(string searchCriteria, Contact updatedContact)
        {
            var contacts = GetAllContacts();
            var contact = contacts.FirstOrDefault(c => c.PhoneNumber == searchCriteria ||
                                                       c.Name.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                                                       c.Surname.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase));

            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(updatedContact.Name))
            {
                contact.Name = updatedContact.Name;
            }
            if (!string.IsNullOrWhiteSpace(updatedContact.Surname))
            {
                contact.Surname = updatedContact.Surname;
            }
            if (!string.IsNullOrWhiteSpace(updatedContact.PhoneNumber))
            {
                contact.PhoneNumber = updatedContact.PhoneNumber;
            }

            SaveContactsToFile(contacts);
            Console.WriteLine("Contact updated successfully.");
        }

        public void DeleteContact(string phoneNumber)
        {
            var contacts = GetAllContacts();
            var contact = contacts.FirstOrDefault(c => c.PhoneNumber == phoneNumber);

            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            contacts.Remove(contact);
            SaveContactsToFile(contacts);
            Console.WriteLine("Contact deleted successfully.");
        }

        public List<Contact> GetAllContacts()
        {
            var contacts = new List<Contact>();

            if (!File.Exists(FilePath))
                return contacts;

            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var data = line.Split(',');
                if (data.Length == 3)
                {
                    contacts.Add(new Contact { Name = data[0], Surname = data[1], PhoneNumber = data[2] });
                }
            }

            return contacts;
        }

        private void SaveContactsToFile(List<Contact> contacts)
        {
            var lines = contacts.Select(c => $"{c.Name},{c.Surname},{c.PhoneNumber}");
            File.WriteAllLines(FilePath, lines);
        }
    }
}
