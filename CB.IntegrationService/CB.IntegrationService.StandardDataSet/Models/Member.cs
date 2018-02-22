using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.StandardDataSet.Models
{
    public class Member
    {
        /// <summary>
        /// Gets or sets the salutation of a user
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        /// Gets or sets the first name of a user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of a user
        /// </summary>
        /// <value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name of a user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the suffix of a user.
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the nickname
        /// </summary>
        public string PrefferedName { get; set; }

        /// <summary>
        /// Gets or sets the type of the member.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the primary language
        /// </summary>
        public string PrimaryLanguage { get; set; }

        /// <summary>
        /// Gets or sets the ethnicity id, which is defined by EB.
        /// </summary>
        public string EthnicityId { get; set; }

        /// <summary>
        /// Gets or sets the race id, which is defined by EB.
        /// </summary>
        public string RaceId { get; set; }

        /// <summary>
        /// Gets or sets the religion id, which is defined by EB. 
        /// </summary>
        public string ReligionId { get; set; }

        /// <summary>
        /// Gets or sets the user’s address. Multiple address can occur, i.e. mailing address, billing address etc.. 
        /// </summary>
        public List<Add> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the user’s phone numbers. Phone numbers can be work, mobile or home.
        /// </summary>
        public List<Phone> Phones { get; set; }

        /// <summary>
        ///  Gets or sets the user’s list of e-mail id.
        /// </summary>
        public List<Email> EmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets the household or family details of a user
        /// </summary>
        public List<Household> Households { get; set; }

        /// <summary>
        /// Gets or sets the contacts of a user. 
        /// A contact refers to the relationship. 
        /// </summary>
        public List<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the list of all the emergency contacts of a user(In case of student). 
        /// </summary>
        public List<EmergencyContact> EmergencyContacts { get; set; }

        /// <summary>
        /// Gets or sets the user’s date of birth. The format  will be in mm/dd/yyyy.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the grade level of the user (In case of student).
        /// </summary>
        public Grade Grade { get; set; }

        /// <summary>
        /// Gets or sets the medical information of a user.
        /// </summary>
        public MedicalInformation MedicalInformation { get; set; }

        /// <summary>
        /// Gets or sets the financial Aid details of a user(In case of student).
        /// </summary>
        public FinancialAid FinancialAid { get; set; }

        /// <summary>
        /// Gets or sets the admission details of a user(In case of student)
        /// </summary>
        public Admission Admission { get; set; }

        /// <summary>
        /// Gets or sets the enrollment details of user(In case of student).
        /// </summary>
        public Enrollment Enrollment { get; set; }
    }

    /// <summary>
    /// Class detailing the address details for a user.
    /// </summary>
    public class Add
    {
        /// <summary>
        /// Gets or sets the type of address. Could be Home, Physical, Billing, Mailing etc
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the line 1 of the address
        /// </summary>
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the line 2 of the address
        /// </summary>
        public string Line2 { get; set; }

        /// <summary>
        /// Gets or sets the name of the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }
    }

    /// <summary>
    /// Class detailing the phone details for a user.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Gets or sets the type of address. eg: Home, Mobile etc
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the extension
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the mobile carrier.
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the status of phone number. Validate phone number and keep the status 
        /// </summary>
        public bool Validated { get; set; }
    }

    /// <summary>
    /// Class detailing the grade details for a user(In case of student).
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// Gets or sets the unique identifier of a grade, which is defined by EB.
        /// </summary>
        public string EBGradeId { get; set; }

        /// <summary>
        /// Gets or sets the grade name.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Class detailing the email details for a user
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Gets or sets the type of email. eg: Primary, Secondary, Business, Personal etc..
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the email id.
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the status of emailId. Validate email id and keep the status
        /// </summary>
        public bool Validated { get; set; }
    }

    /// <summary>
    /// Class detailing the household or family details for a user.
    /// </summary>
    public class Household
    {
        /// <summary>
        /// Gets or sets the product household id.
        /// </summary>
        public string HouseholdId { get; set; }

        /// <summary>
        /// Gets or sets the EBIS household id.
        /// </summary>
        public string EBHouseholdId { get; set; }

        /// <summary>
        /// Gets or sets the name of household.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of members in a household.
        /// </summary>
        public List<UserHousehold> Users { get; set; }

        /// <summary>
        /// Gets or sets the list of household address.
        /// </summary>
        public List<Add> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers of household.
        /// </summary>
        public List<Phone> Phones { get; set; }

        /// <summary>
        /// Gets or sets the email addresses of household.
        /// </summary>
        public List<Email> EmailAddresses { get; set; }
    }

    /// <summary>
    /// Class detailing the relationship of a user.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or sets the product id of a user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the EBIS unique Identifier for a user.
        /// </summary>
        public string EBUserId { get; set; }

        /// <summary>
        /// Gets or sets the first name of a user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of a user
        /// </summary>
        /// <value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name of a user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the primary Phone of a user
        /// </summary>
        /// <value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the Email of a user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the EBIS unique Identifier for given Relation (Father, Mother, Brother, Sister, Spouse)
        /// // Chenge ti type Id 
        /// </summary>
        public string RelationShipType { get; set; }

        /// <summary>
        /// Gets or sets the type of custody a contact has with student (Joint, Sole etc)
        /// </summary>
        public string CustodyType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this relation can be marked as an emergency contact.
        /// </summary>
        public bool IsEmaergencyContact { get; set; }

        /// <summary>
        /// Gets or sets the order while trying for an emergency contact
        /// </summary>
        public int PriorityOrder { get; set; }
    }

    /// <summary>
    /// Class detailing the emergency contact of a user.
    /// </summary>
    public class EmergencyContact
    {
        /// <summary>
        /// Gets or sets the first name of user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of user.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name of user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the relationship to user. Father, neighbour, guardian  etc
        /// </summary>
        public string RelationShip { get; set; }

        /// <summary>
        /// Gets or sets the emergency contact priority. It may be primary/secondary. Save as integer value 1 for primary, 2 for secondary etc
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the list of phone numbers.
        /// </summary>
        public List<Phone> Phones { get; set; }
    }

    /// <summary>
    /// Class detailing the medical information of a user.
    /// </summary>
    public class MedicalInformation
    {
        /// <summary>
        /// Gets or sets the name of insurance provider.
        /// </summary>
        public string InsuranceProvider { get; set; }

        /// <summary>
        /// Gets or sets the insurance number.
        /// </summary>
        public int InsuranceNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of preferred hospitals.
        /// </summary>
        public List<string> PreferredHospitals { get; set; }

        /// <summary>
        /// Gets or sets the list of allergies.
        /// </summary>
        public List<string> Allergies { get; set; }

        /// <summary>
        /// Gets or sets the list of known medical conditions of a user. eg: Asthma
        /// </summary>
        public List<string> KnownConditions { get; set; }
    }

    /// <summary>
    /// Class detailing the financial fid details provided by an institution.
    /// </summary>
    public class FinancialAid
    {
        /// <summary>
        /// Gets or sets the product- user identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the EB- user identifier.
        /// </summary>
        public string EBUserId { get; set; }

        /// <summary>
        /// Gets or sets the product- financial aid unique identifier.
        /// </summary>
        public string FincialAidId { get; set; }

        /// <summary>
        /// Gets or sets the EB- financial aid unique identifier.
        /// </summary>
        public string EBFinancialAidId { get; set; }

        /// <summary>
        /// Gets or sets the student tuition for one academic year.
        /// </summary>
        public Decimal StudentTuition { get; set; }

        /// <summary>
        /// Gets or sets the amount of aid awarded to this family by the school.
        /// </summary>
        public Decimal FinalAward { get; set; }

        /// <summary>
        /// Gets or sets the amount which the family is expected to pay out of pocket.
        /// </summary>
        public Decimal ExpectedFamilyContribution { get; set; }

        /// <summary>
        /// Gets or sets the amount of money that the parents said they could contribute out of pocket.
        /// </summary>
        public Decimal FamilyOfferToPay { get; set; }

        /// <summary>
        /// Gets or sets the amount the student is expected to pay out of pocket.
        /// </summary>
        public Decimal ExpectedApplicantContribution { get; set; }

        /// <summary>
        /// Gets or sets the amount of money that the student said need to  contribute out of pocket.
        /// </summary>
        public Decimal ApplicantOfferToPay { get; set; }

        /// <summary>
        /// Gets or sets the current award proposed by the financial aid staff. This may not be the final award amount granted to the student.
        /// </summary>
        public Decimal RecommendedAward { get; set; }

        /// <summary>
        /// Gets or sets the amount of loans from outside sources.
        /// </summary>
        public Decimal Loan { get; set; }
    }

    /// <summary>
    /// Class detailing the admission details of a user(In case of student).
    /// </summary>
    public class Admission
    {
        /// <summary>
        /// Gets or sets the product - unique identifier of a user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the EBIS unique Identifier of a user.
        /// </summary>
        public string EBUserId { get; set; }

        /// <summary>
        /// Gets or sets the academic year to which student is admitted.
        /// </summary>
        public string AcademicYear { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of application.
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of previous school.
        /// </summary>
        public string EBCurrentSchoolId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the current grade level of the student.
        /// </summary>
        public string EBCurrentGradeId { get; set; }

        /// <summary>
        /// Gets or sets the grade to which the student is getting admission.
        /// </summary>
        public string EBApplyingGradeId { get; set; }

        /// <summary>
        /// Gets or sets the status of application. Eg:Applied,Accepted etc.
        /// </summary>
        public string ApplicationStatus { get; set; }
    }

    /// <summary>
    /// Class detailing the enrollment details of a user(In case of student).
    /// </summary>
    public class Enrollment
    {
        /// <summary>
        /// Gets or sets the product user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the EBIS user id. 
        /// </summary>
        public string EBUserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of application.
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the grade to which the student is getting added.
        /// </summary>
        public string EBApplyingGradeId { get; set; }

        /// <summary>
        /// Gets or sets the status of student application. Eg:Enrolled, withdrawn etc.
        /// </summary>
        public string EnrollmentStatus { get; set; }

        /// <summary>
        /// Gets or sets EBIS previous school identifier.
        /// </summary>
        public string EBPreviousSchoolId { get; set; }

        /// <summary>
        /// Gets or sets the previous grade level of the student.
        /// </summary>
        public string EBPreviousSchoolGradeId { get; set; }
    }

    /// <summary>
    /// Class detailing the information of a member associated with a household.
    /// </summary>
    public class UserHousehold
    {
        /// <summary>
        /// Gets or sets the product user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the EBIS identifier of the user. 
        /// </summary>
        public string EBUserId { get; set; }

        /// <summary>
        /// Gets or sets the role of the user with respective to the student .Eg:Parent, Sibling, Guardian etc
        /// </summary>
        public string EBUserRoleId { get; set; }
    }
}
