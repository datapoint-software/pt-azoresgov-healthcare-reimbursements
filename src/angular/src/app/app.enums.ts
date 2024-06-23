export enum EntityNature {
  Administrative = 65,
  HealthCenter = 67,
  Hospital = 72,
  Office = 79
};

// TODO <joao.pl.lopes> enum options
export enum ProcessPaymentMethod {
  Cash,
  Cheque,
  WireTransfer
};

// TODO <joao.pl.lopes> enum options
export enum ProcessPaymentRecipient {
  LegalRepresentative,
  Patient
}

export enum ProcessStatus {
  Capture = 67
};

export enum UserRoleNature {
  Administrative = 65,
  Administrator = 83,
  Encoder = 69,
  Treasurer = 84,
  Validator = 86,
  WireTransferAgent = 87
};
