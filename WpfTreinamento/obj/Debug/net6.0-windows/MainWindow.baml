   M S B A M L   `   `   `  ����I  EWpfTreinamento, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfTreinamentoWpfTreinamento  5   	   G(clr-namespace:WpfTreinamento.ConversoresWpfTreinamento.Conversores  5   	       WpfTreinamento.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:WpfTreinamento     5   	   ?
converters(clr-namespace:WpfTreinamento.Conversores     5   	     � Title$  Consulta de Times��5	   	   $	��450��6#   $	��800��60   $��Stretch=�6<   $��Stretch=�6a    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�6�    � WindowStyle$ 
ToolWindow=�6�    � 	Resources 5      :   4WpfTreinamento.Conversores.TimeMultiBindingConverter   TimeMultiBindingConverter��%   &	          5   
   6/   5      .��5      � $
}�True��6   $	��450��6   .+�5   
   �� $��Center=�6   $��74��61   
���   �$	��Top=�6L   $	��800��6d   ���	#FF07A9636p   �� 5   
   $��Consulta de Times��6   $��Center=�6,   $��0,20,0,0q�6I   $	��Top=�6[   
�������$��18(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ listgrid��5      $	��265��6x   $	��Top=�6�   $��0,74,0,0q�6�   $��14(�6�   ��6%   �� *
timeList+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=� �� View 5      �� 5      .P�5      �� $��ID��6%   	 �� DisplayMemberBinding	 61   �� 
 �� Path$
 ID��� 5      $
��Nome��6%   	 63   �� $

 Nome��� 5      $��Divisão��6%   	 66   �� $
 Divisao��� 5      $��Região��6%   	 65   �� $
 Regiao��� 5      $��
Campeonato��6%   	 69   �� $
 NomeCampeonato�5      5      �� 5   
   $��Remover��6_   $
��Left=�6q   $��507,362,0,0q�6�   $	��Top=�6�   $��36��6�   $	��162��6�   $��16(�6�    J� RenderTransformOrigin$ 0.452,-0.278&�6�   ���	#FF07A963
����������	#FF067545 �� 	ClickMode$ Release=�6F  ��6   ��  �� ElementName$ listgrid����6C   �� *	deletar+6F  �� 5   
   $��	Adicionar��6�   $
��Left=�6�   $��120,362,0,0q�6�   $	��Top=�6�   $	��162��6�   $��36��6�   $��16(�6�   ���	#FF07A963
����������	#FF067545��6   ��  �� RelativeSource ��  �� AncestorType" ���6k   �� *abrirTelaFT+67  �� 5   
   $��Editar��6   $��Center=�6B   $��	0,362,0,0q�6_   $	��Top=�6r   $��36��6�   $	��162��6�   $��16(�6�   ���	#FF07A963
����������	#FF067545��6"   �� *abrirTelaFT+��5       `� 5!       `� 	Converter# [  6    `� Mode$ TwoWay=�6b   .1�5"      ��  6   �� 
 Window�� 5#      $ listgrid��6   5$      5&      5'      5(      