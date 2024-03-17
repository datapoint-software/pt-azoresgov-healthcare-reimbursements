import { FormControl } from "@angular/forms";

export const invalid = (control: FormControl) =>

  control.enabled &&
  control.invalid &&
  (control.dirty || control.touched);

export const setControlsEnabled = (enabled: boolean, controls: Array<FormControl>) => {

  const co = { emitEvent: false };

  const fn = (enabled) ?
    ((c: FormControl) => c.enable(co)) :
    ((c: FormControl) => c.disable(co));

  for (let control of controls)
    fn(control);
};

export const parseBoolean = (subject: string | boolean | null | undefined): boolean | null | undefined => {

  if (subject === null || subject === undefined)
    return null;

  if (subject === true || subject === false)
    return subject;

  if (subject === 'true')
    return true;

  if (subject === 'false')
    return false;

  return undefined;
};
