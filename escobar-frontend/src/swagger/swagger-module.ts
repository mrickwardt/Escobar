import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import * as ServiceProxies from './swag-proxy';

@NgModule({
  imports: [HttpClientModule],
  providers: [
    ServiceProxies.AccountServiceProxy,
    ServiceProxies.HistoryServiceProxy
  ]
})
export class SwaggerModule { }
