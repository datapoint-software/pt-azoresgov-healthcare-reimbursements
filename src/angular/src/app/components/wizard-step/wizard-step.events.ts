export class WizardStepNextEvent extends Event {
  constructor(_eventInitDict?: EventInit) {
    super('wizardStepNextEvent', _eventInitDict);
  }
}

export class WizardStepPreviousEvent extends Event {
  constructor(_eventInitDict?: EventInit) {
    super('wizardStepPreviousEvent', _eventInitDict);
  }
}
