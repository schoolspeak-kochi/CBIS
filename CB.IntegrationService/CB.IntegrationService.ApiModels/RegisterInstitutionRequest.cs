/* 
 * Education Brands Integration Service APIs
 *
 * The integration framework is an attempt to define a standard and simple socket each brand can plug in to interact with other EB products. It defines what each product needs to do to integrate with other products. It has a set of API to interact with other products and what each product should implement to receive communication from other products. This is an API specification detailing the APIs for Education Brands IntegrationService.  Most of these APIs will be implemented in EBIS.  The APIs in the 'Product Endpoints' section has to be implemented by each of the Products.  <b>NOTE - <i>This specification is still in early development stage and is subject to change without notice.</i></b> 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: sobin@schoolspeak.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CB.IntegrationService.ApiModels
{
    /// <summary>
    /// RegisterInstitutionRequest
    /// </summary>
    [DataContract]
    public partial class RegisterInstitutionRequest :  IEquatable<RegisterInstitutionRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterInstitutionRequest" /> class.
        /// </summary>
        /// <param name="ProductName">Name of the Product.</param>
        /// <param name="ProductId">Education brands Id for the product.</param>
        /// <param name="InstitutionName">Name of the institution.</param>
        /// <param name="InstitutionId">The local product-specific id of the institution.</param>
        /// <param name="Address">Address.</param>
        /// <param name="Phone">the phone number of the insttution.</param>
        /// <param name="WebAddress">The website address of the institution.</param>
        /// <param name="ServiceOffered">A list of services offered  by the product.</param>
        public RegisterInstitutionRequest(string ProductName = default(string), string ProductId = default(string), string InstitutionName = default(string), string InstitutionId = default(string), List<Address> Address = default(List<Address>), string Phone = default(string), string WebAddress = default(string), List<string> ServiceOffered = default(List<string>))
        {
            this.ProductName = ProductName;
            this.ProductId = ProductId;
            this.InstitutionName = InstitutionName;
            this.InstitutionId = InstitutionId;
            this.Address = Address;
            this.Phone = Phone;
            this.WebAddress = WebAddress;
            this.ServiceOffered = ServiceOffered;
        }
        
        /// <summary>
        /// Name of the Product
        /// </summary>
        /// <value>Name of the Product</value>
        [DataMember(Name="productName", EmitDefaultValue=false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Education brands Id for the product
        /// </summary>
        /// <value>Education brands Id for the product</value>
        [DataMember(Name="productId", EmitDefaultValue=false)]
        public string ProductId { get; set; }

        /// <summary>
        /// Name of the institution
        /// </summary>
        /// <value>Name of the institution</value>
        [DataMember(Name="institutionName", EmitDefaultValue=false)]
        public string InstitutionName { get; set; }

        /// <summary>
        /// The local product-specific id of the institution
        /// </summary>
        /// <value>The local product-specific id of the institution</value>
        [DataMember(Name="InstitutionId", EmitDefaultValue=false)]
        public string InstitutionId { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name="Address", EmitDefaultValue=false)]
        public List<Address> Address { get; set; }

        /// <summary>
        /// the phone number of the insttution
        /// </summary>
        /// <value>the phone number of the insttution</value>
        [DataMember(Name="phone", EmitDefaultValue=false)]
        public string Phone { get; set; }

        /// <summary>
        /// The website address of the institution
        /// </summary>
        /// <value>The website address of the institution</value>
        [DataMember(Name="webAddress", EmitDefaultValue=false)]
        public string WebAddress { get; set; }

        /// <summary>
        /// A list of services offered  by the product
        /// </summary>
        /// <value>A list of services offered  by the product</value>
        [DataMember(Name="serviceOffered", EmitDefaultValue=false)]
        public List<string> ServiceOffered { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RegisterInstitutionRequest {\n");
            sb.Append("  ProductName: ").Append(ProductName).Append("\n");
            sb.Append("  ProductId: ").Append(ProductId).Append("\n");
            sb.Append("  InstitutionName: ").Append(InstitutionName).Append("\n");
            sb.Append("  InstitutionId: ").Append(InstitutionId).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  WebAddress: ").Append(WebAddress).Append("\n");
            sb.Append("  ServiceOffered: ").Append(ServiceOffered).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as RegisterInstitutionRequest);
        }

        /// <summary>
        /// Returns true if RegisterInstitutionRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of RegisterInstitutionRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RegisterInstitutionRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ProductName == input.ProductName ||
                    (this.ProductName != null &&
                    this.ProductName.Equals(input.ProductName))
                ) && 
                (
                    this.ProductId == input.ProductId ||
                    (this.ProductId != null &&
                    this.ProductId.Equals(input.ProductId))
                ) && 
                (
                    this.InstitutionName == input.InstitutionName ||
                    (this.InstitutionName != null &&
                    this.InstitutionName.Equals(input.InstitutionName))
                ) && 
                (
                    this.InstitutionId == input.InstitutionId ||
                    (this.InstitutionId != null &&
                    this.InstitutionId.Equals(input.InstitutionId))
                ) && 
                (
                    this.Address == input.Address ||
                    this.Address != null &&
                    this.Address.SequenceEqual(input.Address)
                ) && 
                (
                    this.Phone == input.Phone ||
                    (this.Phone != null &&
                    this.Phone.Equals(input.Phone))
                ) && 
                (
                    this.WebAddress == input.WebAddress ||
                    (this.WebAddress != null &&
                    this.WebAddress.Equals(input.WebAddress))
                ) && 
                (
                    this.ServiceOffered == input.ServiceOffered ||
                    this.ServiceOffered != null &&
                    this.ServiceOffered.SequenceEqual(input.ServiceOffered)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.ProductName != null)
                    hashCode = hashCode * 59 + this.ProductName.GetHashCode();
                if (this.ProductId != null)
                    hashCode = hashCode * 59 + this.ProductId.GetHashCode();
                if (this.InstitutionName != null)
                    hashCode = hashCode * 59 + this.InstitutionName.GetHashCode();
                if (this.InstitutionId != null)
                    hashCode = hashCode * 59 + this.InstitutionId.GetHashCode();
                if (this.Address != null)
                    hashCode = hashCode * 59 + this.Address.GetHashCode();
                if (this.Phone != null)
                    hashCode = hashCode * 59 + this.Phone.GetHashCode();
                if (this.WebAddress != null)
                    hashCode = hashCode * 59 + this.WebAddress.GetHashCode();
                if (this.ServiceOffered != null)
                    hashCode = hashCode * 59 + this.ServiceOffered.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
