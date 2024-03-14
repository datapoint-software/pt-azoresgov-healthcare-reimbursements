export interface IdentityResultModel {
  user: IdentityUserResultModel;
}

export interface IdentityUserResultModel {
  id: string;
  name: string;
  emailAddress: string;
}
