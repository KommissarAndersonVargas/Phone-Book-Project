using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PersonInformation
{
    public string cpf { get; set; }
    public string fullName { get; set; }
    public string cellPhoneNumber { get; set; }
    public string email { get; set; }
    public string address { get; set; }
    public DateTime Hora_da_entrada { get; set; }



    public PersonInformation(string cpf, string fullName, string cellPhoneNumber, string email, string address, DateTime hora_da_entrada)
    {
        this.cpf = cpf.ToLower();
        this.fullName = fullName.ToLower();
        this.cellPhoneNumber = cellPhoneNumber.ToLower();
        this.email = email.ToLower();
        this.address = address.ToLower();
        Hora_da_entrada = hora_da_entrada;
    }

    public IEnumerable<PersonInformation> GetOrderByLastName(BindingList<PersonInformation> infoList)
    {

        var resultByLastName = infoList.OrderBy(x => x.fullName);
        return resultByLastName;
    }
    public IEnumerable<PersonInformation> GetOrderByAddress(BindingList<PersonInformation> infoList)
    {

        var resultByLastName = infoList.OrderBy(x => x.address);
        return resultByLastName;
    }
    public IEnumerable<PersonInformation> GetOrderByCpf(BindingList<PersonInformation> infoList)
    {

        var resultByLastName = infoList.OrderBy(x => x.cpf);
        return resultByLastName;
    }
    public PersonInformation() { }


}

