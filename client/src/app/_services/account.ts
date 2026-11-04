import { HttpClient } from '@angular/common/http';
import { Service } from '@angular/core';

@Service()
export class Account {

    baseUrl = 'http://localhost:5001/api/';

    constructor(private http : HttpClient){

    }

    login(model : any)
    {
        return this.http.post(this.baseUrl + 'account/login' , model)
    }
}
