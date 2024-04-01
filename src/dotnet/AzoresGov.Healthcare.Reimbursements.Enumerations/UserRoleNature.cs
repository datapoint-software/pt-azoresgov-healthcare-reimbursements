using System;
using System.Collections.Generic;
namespace AzoresGov.Healthcare.Reimbursements.Enumerations
{
    public enum UserRoleNature
    {
        Administrative = 'A',

        Administrator = 'S',

        Encoder = 'E',

        Validator = 'V',

        Treasurer = 'T',

        WireTransferAgent = 'W'
    }
}
