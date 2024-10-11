export interface ITransaction {
  Id: string;
  MandateId: string;
  ClientId: number;
  OperationType: number;
  FundId: number;
  Date: Date;
  Value: number;
}
