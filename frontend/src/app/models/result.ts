export interface Result<T>{
  isSuccess: boolean,
  errors:Error[],
  response:T
}

export interface Error{
  code:string,
  description?:string
}
