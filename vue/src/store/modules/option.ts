import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Option from '../entities/option'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface OptionState extends ListState<Option>{
    editOption:Option
    currentCategory:string
}
class OptionMutations extends ListMutations<Option>{

}
class OptionModule extends ListModule<OptionState,any,Option>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Option>(),
        loading:false,
        editOption:new Option(),
        currentCategory:''
    }
    actions={
        async getAll(context:ActionContext<OptionState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Option/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Option>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<OptionState,any>,payload:any){
            await Ajax.post('/api/services/app/Option/Create',payload.data);
        },
        async update(context:ActionContext<OptionState,any>,payload:any){
            await Ajax.put('/api/services/app/Option/Update',payload.data);
        },
        async delete(context:ActionContext<OptionState,any>,payload:any){
            await Ajax.delete('/api/services/app/Option/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<OptionState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Option/Get?Id='+payload.id);
            return reponse.data.result as Option;
        }
    };
    mutations={
        setCurrentPage(state:OptionState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:OptionState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:OptionState,option:Option){
            state.editOption=option;
        },
        setCurrentCategory(state:OptionState,category:string){
            state.currentCategory=category;
        }
    }
}
const  optionModule=new OptionModule();
export default optionModule;