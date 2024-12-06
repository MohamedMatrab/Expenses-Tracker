export interface User{
  userName: string
  firstName:string
  imagePath:string
  lastName:string
  email:string
  birthDate:string
}

export interface LoginDto{
  userName:string,
  password:string
}

export interface TokenStored{
  token: string,
  expirationDate:Date,
  refreshToken:string,
}

export interface VerifyToken{
  userId?:string,
  success:boolean,
  errors:string[]
}

export interface AuthResult{
  token?: string,
  expirationDate?:Date,
  refreshToken?:string,
  success:boolean,
  errors:string[]
}

export interface RefreshTokenDto{
  token:string,
  refreshToken:string
}
