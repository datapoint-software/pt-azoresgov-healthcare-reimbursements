import { createFeatureSelector } from "@ngrx/store";
import { featureName } from "./identity.constants";
import { IdentityState } from "./identity.state";

export const state = createFeatureSelector<IdentityState>(featureName);
