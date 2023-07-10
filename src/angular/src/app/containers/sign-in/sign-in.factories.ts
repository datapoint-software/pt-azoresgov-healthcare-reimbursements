export const createSignInUrl = (redirectUrl: string) =>
  `/sign-in?forward=${encodeURIComponent(redirectUrl)}`;

