// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: grpc5team.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace IconicThon.Network {
  public static partial class Attendance
  {
    static readonly string __ServiceName = "IconicThon.Network.Attendance";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::IconicThon.Network.LectureInfo> __Marshaller_IconicThon_Network_LectureInfo = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::IconicThon.Network.LectureInfo.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::IconicThon.Network.PrivateKey> __Marshaller_IconicThon_Network_PrivateKey = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::IconicThon.Network.PrivateKey.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::IconicThon.Network.JsonRequest> __Marshaller_IconicThon_Network_JsonRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::IconicThon.Network.JsonRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::IconicThon.Network.JsonResponse> __Marshaller_IconicThon_Network_JsonResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::IconicThon.Network.JsonResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::IconicThon.Network.RequestAttendanceInfo> __Marshaller_IconicThon_Network_RequestAttendanceInfo = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::IconicThon.Network.RequestAttendanceInfo.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::IconicThon.Network.ResponseAttendanceInfo> __Marshaller_IconicThon_Network_ResponseAttendanceInfo = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::IconicThon.Network.ResponseAttendanceInfo.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::IconicThon.Network.LectureInfo, global::IconicThon.Network.PrivateKey> __Method_RefreshPrivateKey = new grpc::Method<global::IconicThon.Network.LectureInfo, global::IconicThon.Network.PrivateKey>(
        grpc::MethodType.Unary,
        __ServiceName,
        "RefreshPrivateKey",
        __Marshaller_IconicThon_Network_LectureInfo,
        __Marshaller_IconicThon_Network_PrivateKey);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::IconicThon.Network.JsonRequest, global::IconicThon.Network.JsonResponse> __Method_RequestLectureList = new grpc::Method<global::IconicThon.Network.JsonRequest, global::IconicThon.Network.JsonResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "RequestLectureList",
        __Marshaller_IconicThon_Network_JsonRequest,
        __Marshaller_IconicThon_Network_JsonResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::IconicThon.Network.RequestAttendanceInfo, global::IconicThon.Network.ResponseAttendanceInfo> __Method_SendAttendanceInfo = new grpc::Method<global::IconicThon.Network.RequestAttendanceInfo, global::IconicThon.Network.ResponseAttendanceInfo>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SendAttendanceInfo",
        __Marshaller_IconicThon_Network_RequestAttendanceInfo,
        __Marshaller_IconicThon_Network_ResponseAttendanceInfo);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::IconicThon.Network.Grpc5TeamReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Attendance</summary>
    [grpc::BindServiceMethod(typeof(Attendance), "BindService")]
    public abstract partial class AttendanceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::IconicThon.Network.PrivateKey> RefreshPrivateKey(global::IconicThon.Network.LectureInfo request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::IconicThon.Network.JsonResponse> RequestLectureList(global::IconicThon.Network.JsonRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::IconicThon.Network.ResponseAttendanceInfo> SendAttendanceInfo(global::IconicThon.Network.RequestAttendanceInfo request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Attendance</summary>
    public partial class AttendanceClient : grpc::ClientBase<AttendanceClient>
    {
      /// <summary>Creates a new client for Attendance</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public AttendanceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Attendance that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public AttendanceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected AttendanceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected AttendanceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::IconicThon.Network.PrivateKey RefreshPrivateKey(global::IconicThon.Network.LectureInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RefreshPrivateKey(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::IconicThon.Network.PrivateKey RefreshPrivateKey(global::IconicThon.Network.LectureInfo request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_RefreshPrivateKey, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::IconicThon.Network.PrivateKey> RefreshPrivateKeyAsync(global::IconicThon.Network.LectureInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RefreshPrivateKeyAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::IconicThon.Network.PrivateKey> RefreshPrivateKeyAsync(global::IconicThon.Network.LectureInfo request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_RefreshPrivateKey, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::IconicThon.Network.JsonResponse RequestLectureList(global::IconicThon.Network.JsonRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RequestLectureList(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::IconicThon.Network.JsonResponse RequestLectureList(global::IconicThon.Network.JsonRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_RequestLectureList, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::IconicThon.Network.JsonResponse> RequestLectureListAsync(global::IconicThon.Network.JsonRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RequestLectureListAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::IconicThon.Network.JsonResponse> RequestLectureListAsync(global::IconicThon.Network.JsonRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_RequestLectureList, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::IconicThon.Network.ResponseAttendanceInfo SendAttendanceInfo(global::IconicThon.Network.RequestAttendanceInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SendAttendanceInfo(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::IconicThon.Network.ResponseAttendanceInfo SendAttendanceInfo(global::IconicThon.Network.RequestAttendanceInfo request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SendAttendanceInfo, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::IconicThon.Network.ResponseAttendanceInfo> SendAttendanceInfoAsync(global::IconicThon.Network.RequestAttendanceInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SendAttendanceInfoAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::IconicThon.Network.ResponseAttendanceInfo> SendAttendanceInfoAsync(global::IconicThon.Network.RequestAttendanceInfo request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SendAttendanceInfo, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override AttendanceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new AttendanceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(AttendanceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_RefreshPrivateKey, serviceImpl.RefreshPrivateKey)
          .AddMethod(__Method_RequestLectureList, serviceImpl.RequestLectureList)
          .AddMethod(__Method_SendAttendanceInfo, serviceImpl.SendAttendanceInfo).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, AttendanceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_RefreshPrivateKey, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::IconicThon.Network.LectureInfo, global::IconicThon.Network.PrivateKey>(serviceImpl.RefreshPrivateKey));
      serviceBinder.AddMethod(__Method_RequestLectureList, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::IconicThon.Network.JsonRequest, global::IconicThon.Network.JsonResponse>(serviceImpl.RequestLectureList));
      serviceBinder.AddMethod(__Method_SendAttendanceInfo, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::IconicThon.Network.RequestAttendanceInfo, global::IconicThon.Network.ResponseAttendanceInfo>(serviceImpl.SendAttendanceInfo));
    }

  }
}
#endregion
