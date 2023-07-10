export interface IdentityRefreshModel {
  refreshToken: string;
}

export interface IdentityRefreshResultModel {
  accessToken: string;
  accessTokenExpiration: number;
  refreshToken: string;
}
