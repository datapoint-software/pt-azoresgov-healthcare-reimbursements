import { SignInFormSubmitPayload } from "./sign-in-form.payloads";

export class SignInFormSubmitEvent extends Event {
  constructor(public readonly payload: SignInFormSubmitPayload, _eventInitDict?: EventInit) {
    super('signInFormSubmitEvent', _eventInitDict);
  }
}
