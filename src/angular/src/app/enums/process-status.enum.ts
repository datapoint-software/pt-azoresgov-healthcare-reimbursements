export enum ProcessStatus {
  Capture = ('C'.charCodeAt(0)),
  DocumentUpload = ('D'.charCodeAt(0)),
  Codification = ('O'.charCodeAt(0)),
  Validation = ('V'.charCodeAt(0)),
  Payment = ('P'.charCodeAt(0)),
  Complete = ('M'.charCodeAt(0)),
  Cancelled = ('A'.charCodeAt(0))
}
