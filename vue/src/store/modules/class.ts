import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Class from '../entities/class'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface ClassState extends ListState<Class>{
    editClass:Class
}
class ClassMutations extends ListMutations<Class>{

}
class ClassModule extends ListModule<ClassState,any,Class>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Class>(),
        loading:false,
        editClass:new Class()
    }
    actions={
        async getAll(context:ActionContext<ClassState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Class/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Class>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<ClassState,any>,payload:any){
            await Ajax.post('/api/services/app/Class/Create',payload.data);
        },
        async update(context:ActionContext<ClassState,any>,payload:any){
            await Ajax.put('/api/services/app/Class/Update',payload.data);
        },
        async delete(context:ActionContext<ClassState,any>,payload:any){
            await Ajax.delete('/api/services/app/Class/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<ClassState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Class/Get?Id='+payload.id);
            return reponse.data.result as Class;
        },
        async addOrder(context:ActionContext<ClassState,any>,payload:any){
            await Ajax.post('/api/services/app/Class/AddOrders',payload.data);
        },
        async finish(context: ActionContext<ClassState, any>, payload: any) {
            await Ajax.post('/api/services/app/Class/Finish', payload.data);
        },
    };
    mutations={
        setCurrentPage(state:ClassState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:ClassState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:ClassState,clas:Class){
            state.editClass=clas;
        }
    }
}
const  classModule=new ClassModule();
export default classModule;