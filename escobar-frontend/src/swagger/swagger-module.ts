import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

 import * as ServiceProxies from './swag-proxy'

@NgModule({
  imports: [HttpClientModule],
  providers: [
    ServiceProxies.CountryServiceProxy,
    ServiceProxies.ServiceProxy
  ]
})
export class SwaggerModule { }
