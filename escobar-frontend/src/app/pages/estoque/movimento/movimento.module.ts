import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { MovimentoRoutingModule } from './movimento-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { FlexLayoutModule } from '@angular/flex-layout';
import {MatDividerModule} from '@angular/material/divider';
import { MovimentoComponent } from './movimento.component';
import { MatTabsModule } from '@angular/material/tabs';
import { EditComponent } from './edit/edit.component';
import { MatTableModule } from '@angular/material/table';

@NgModule({
  declarations: [CreateComponent, MovimentoComponent, EditComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatTabsModule,
    MatTableModule,
    FlexLayoutModule,
    MovimentoRoutingModule
  ]
})
export class MovimentoModule { }
