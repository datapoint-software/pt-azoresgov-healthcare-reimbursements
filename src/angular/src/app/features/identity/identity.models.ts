export interface IdentityRefreshEntityResultModel {
  id: string;
  permissions: IdentityRefreshPermissionResultModel[];
}

export interface IdentityRefreshPermissionResultModel {
  id: string;
  name: string;
}

export interface IdentityRefreshResultModel {
  entities: IdentityRefreshEntityResultModel[];
  permissions: IdentityRefreshPermissionResultModel[];
  user: IdentityRefreshUserResultModel;
  userSession: IdentityRefreshUserSessionResultModel;
}

export interface IdentityRefreshUserResultModel {
  id: string;
  name: string;
}

export interface IdentityRefreshUserSessionResultModel {
  id: string;
}
