﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Quantum {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Quantum.IQuantumOperations")]
    public interface IQuantumOperations {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuantumOperations/UpdateOrderStatus", ReplyAction="http://tempuri.org/IQuantumOperations/UpdateOrderStatusResponse")]
        bool UpdateOrderStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQuantumOperations/UpdateOrderStatus", ReplyAction="http://tempuri.org/IQuantumOperations/UpdateOrderStatusResponse")]
        System.Threading.Tasks.Task<bool> UpdateOrderStatusAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQuantumOperationsChannel : WebApplication2.Quantum.IQuantumOperations, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QuantumOperationsClient : System.ServiceModel.ClientBase<WebApplication2.Quantum.IQuantumOperations>, WebApplication2.Quantum.IQuantumOperations {
        
        public QuantumOperationsClient() {
        }
        
        public QuantumOperationsClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QuantumOperationsClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuantumOperationsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuantumOperationsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool UpdateOrderStatus() {
            return base.Channel.UpdateOrderStatus();
        }
        
        public System.Threading.Tasks.Task<bool> UpdateOrderStatusAsync() {
            return base.Channel.UpdateOrderStatusAsync();
        }
    }
}
