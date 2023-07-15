export interface IdentityState {
  claims?: {
    entities: Array<{
      id: string;
      permissions: Array<{
        id: string;
        name: string;
      }>;
    }>;
    permissions: Array<{
      id: string;
      name: string;
    }>;
    user: {
      id: string;
      name: string;
    };
    userSession: {
      id: string;
    };
  };
}
