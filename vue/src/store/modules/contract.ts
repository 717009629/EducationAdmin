import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Contract from '../entities/contract'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'
import Audite from '../entities/audite'
interface ContractState extends ListState<Contract>{
    editContract:Contract
}
class ContractMutations extends ListMutations<Contract>{

}
class ContractModule extends ListModule<ContractState,any,Contract>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Contract>(),
        loading:false,
        editContract:new Contract()
    }
    actions={
        async getAll(context:ActionContext<ContractState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Contract/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Contract>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<ContractState,any>,payload:any){
            await Ajax.post('/api/services/app/Contract/Create',payload.data);
        },
        async update(context:ActionContext<ContractState,any>,payload:any){
            await Ajax.put('/api/services/app/Contract/Update',payload.data);
        },
        async delete(context:ActionContext<ContractState,any>,payload:any){
            await Ajax.delete('/api/services/app/Contract/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<ContractState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Contract/Get?Id='+payload.id);
            return reponse.data.result as Contract;
        },
        async audite(context:ActionContext<ContractState,any>,payload:any){
            await Ajax.post('/api/services/app/Contract/Audite',payload.data);
        },
    };
    mutations={
        setCurrentPage(state:ContractState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:ContractState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:ContractState,contract:Contract){
            state.editContract=contract;
        }
    }
}
const  contractModule=new ContractModule();
export default contractModule;