﻿/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.0.6.0 (NJsonSchema v10.0.23.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

import * as moment from 'moment';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class AccountServiceProxy {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    /**
     * @param user (optional) 
     * @return Success
     */
    register(user: UserDtoRegister | null | undefined): Observable<User> {
        let url_ = this.baseUrl + "/account/register";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(user);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json", 
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processRegister(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processRegister(<any>response_);
                } catch (e) {
                    return <Observable<User>><any>_observableThrow(e);
                }
            } else
                return <Observable<User>><any>_observableThrow(response_);
        }));
    }

    protected processRegister(response: HttpResponseBase): Observable<User> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = User.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<User>(<any>null);
    }

    /**
     * @return Success
     */
    user(): Observable<string> {
        let url_ = this.baseUrl + "/account/user";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUser(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUser(<any>response_);
                } catch (e) {
                    return <Observable<string>><any>_observableThrow(e);
                }
            } else
                return <Observable<string>><any>_observableThrow(response_);
        }));
    }

    protected processUser(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<string>(<any>null);
    }

    /**
     * @param userID (optional) 
     * @return Success
     */
    history(userID: string | null | undefined): Observable<UserAccess[]> {
        let url_ = this.baseUrl + "/account/history?";
        if (userID !== undefined)
            url_ += "UserID=" + encodeURIComponent("" + userID) + "&"; 
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processHistory(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processHistory(<any>response_);
                } catch (e) {
                    return <Observable<UserAccess[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<UserAccess[]>><any>_observableThrow(response_);
        }));
    }

    protected processHistory(response: HttpResponseBase): Observable<UserAccess[]> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(UserAccess.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<UserAccess[]>(<any>null);
    }
}

export class UserDtoRegister implements IUserDtoRegister {
    nome!: string | undefined;
    email!: string | undefined;
    login!: string | undefined;
    senha!: string | undefined;
    cpf!: string | undefined;

    constructor(data?: IUserDtoRegister) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.nome = data["nome"];
            this.email = data["email"];
            this.login = data["login"];
            this.senha = data["senha"];
            this.cpf = data["cpf"];
        }
    }

    static fromJS(data: any): UserDtoRegister {
        data = typeof data === 'object' ? data : {};
        let result = new UserDtoRegister();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["nome"] = this.nome;
        data["email"] = this.email;
        data["login"] = this.login;
        data["senha"] = this.senha;
        data["cpf"] = this.cpf;
        return data; 
    }
}

export interface IUserDtoRegister {
    nome: string | undefined;
    email: string | undefined;
    login: string | undefined;
    senha: string | undefined;
    cpf: string | undefined;
}

export class User implements IUser {
    id!: string | undefined;
    nome!: string | undefined;
    email!: string | undefined;
    login!: string | undefined;
    senha!: string | undefined;
    senhaHash!: string | undefined;
    cpf!: string | undefined;

    constructor(data?: IUser) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.nome = data["nome"];
            this.email = data["email"];
            this.login = data["login"];
            this.senha = data["senha"];
            this.senhaHash = data["senhaHash"];
            this.cpf = data["cpf"];
        }
    }

    static fromJS(data: any): User {
        data = typeof data === 'object' ? data : {};
        let result = new User();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["nome"] = this.nome;
        data["email"] = this.email;
        data["login"] = this.login;
        data["senha"] = this.senha;
        data["senhaHash"] = this.senhaHash;
        data["cpf"] = this.cpf;
        return data; 
    }
}

export interface IUser {
    id: string | undefined;
    nome: string | undefined;
    email: string | undefined;
    login: string | undefined;
    senha: string | undefined;
    senhaHash: string | undefined;
    cpf: string | undefined;
}

export class UserAccess implements IUserAccess {
    id!: string | undefined;
    dataAccess!: moment.Moment | undefined;
    success!: boolean | undefined;
    userID!: string | undefined;
    log!: string | undefined;

    constructor(data?: IUserAccess) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.dataAccess = data["dataAccess"] ? moment(data["dataAccess"].toString()) : <any>undefined;
            this.success = data["success"];
            this.userID = data["userID"];
            this.log = data["log"];
        }
    }

    static fromJS(data: any): UserAccess {
        data = typeof data === 'object' ? data : {};
        let result = new UserAccess();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["dataAccess"] = this.dataAccess ? this.dataAccess.toISOString() : <any>undefined;
        data["success"] = this.success;
        data["userID"] = this.userID;
        data["log"] = this.log;
        return data; 
    }
}

export interface IUserAccess {
    id: string | undefined;
    dataAccess: moment.Moment | undefined;
    success: boolean | undefined;
    userID: string | undefined;
    log: string | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number; 
    response: string; 
    headers: { [key: string]: any; };
    result: any; 

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader(); 
            reader.onload = event => { 
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob); 
        }
    });
}