import { EnvironmentNature } from "../../../enums/environment-nature.enum";

export interface EnvironmentState {
  nature: EnvironmentNature;
  productVersion: string;
}
