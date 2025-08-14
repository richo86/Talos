export interface credencialesUsuario{
    email: string;
    password: string;
}

export interface respuestaAutenticacion {
    token: string;
    expiracion: Date;
}

export interface usuarioDTO{
    id: string;
    email: string;
    userName: string;
    phoneNumber: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    middleName: string;
    firstLastName: string;
    secondLastName: string;
    address: string;
    country: string;
    gender: string;
    roles: string;
}