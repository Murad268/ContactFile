using System;
using ContactFile.Interfaces;
using ContactFile.Models;
using ContactFile.Controllers;

class Program
{
    static void Main()
    {
        IContactManager contactManager = new ContactController();

        while (true)
        {
            Console.WriteLine("\n1. Add Contact\n2. Edit Contact\n3. Delete Contact\n4. Show All Contacts\n5. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var name = GetValidInput("Enter Name: ");
                    var surname = GetValidInput("Enter Surname: ");
                    var phoneNumber = GetValidInput("Enter Phone Number: ");
                    contactManager.AddContact(new Contact { Name = name, Surname = surname, PhoneNumber = phoneNumber });
                    break;

                case "2":
                    ShowAllContacts(contactManager);
                    Console.WriteLine("Edit Contact - Search by Name, Surname, or Phone Number");
                    var searchCriteria = GetValidInput("Enter Name, Surname, or Phone Number: ");
                    Console.WriteLine("\nSelect the field to edit:");
                    Console.WriteLine("1. Name\n2. Surname\n3. Phone Number");
                    var editChoice = Console.ReadLine();

                    Contact updatedContact = new Contact();
                    switch (editChoice)
                    {
                        case "1":
                            updatedContact.Name = GetValidInput("Enter New Name: ");
                            break;

                        case "2":
                            updatedContact.Surname = GetValidInput("Enter New Surname: ");
                            break;

                        case "3":
                            updatedContact.PhoneNumber = GetValidInput("Enter New Phone Number: ");
                            break;

                        default:
                            Console.WriteLine("Invalid option.");
                            continue;
                    }

                    contactManager.EditContact(searchCriteria, updatedContact);
                    break;

                case "3":
                    ShowAllContacts(contactManager);
                    var deletePhoneNumber = GetValidInput("Enter Phone Number of Contact to Delete: ");
                    Console.Write("Are you sure you want to delete this contact? (yes/no): ");
                    var confirmation = Console.ReadLine();
                    if (confirmation?.ToLower() == "yes")
                    {
                        contactManager.DeleteContact(deletePhoneNumber);
                    }
                    else
                    {
                        Console.WriteLine("Contact deletion cancelled.");
                    }
                    break;

                case "4":
                    ShowAllContacts(contactManager);
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }

    private static string GetValidInput(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("This field is required. Please enter a value.");
            }
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    private static void ShowAllContacts(IContactManager contactManager)
    {
        Console.WriteLine("\n--- All Contacts ---");
        var contacts = (contactManager as ContactController).GetAllContacts();  
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found.");
        }
        else
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Name}, Surname: {contact.Surname}, Phone Number: {contact.PhoneNumber}");
            }
        }
        Console.WriteLine("--------------------");
    }
}
