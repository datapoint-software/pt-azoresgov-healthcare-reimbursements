import { ErrorModel } from "../../app.models";
import { SignInState } from "./sign-in.state";

export interface SignInPayload {
  emailAddress: string;
  password: string;
  persistent: boolean;
}

export interface SignInConfigurePayload extends SignInState {

}

export interface SignInErrorPayload extends ErrorModel {

}
