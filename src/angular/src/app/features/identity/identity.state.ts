export interface IdentityState {
  secrets?: {
    accessToken: string;
    accessTokenExpiration: number;
    persistent: boolean;
    refreshToken: string;
  }
}
